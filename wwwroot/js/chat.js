function toggleChat() {
    const box = document.getElementById('chat-box');
    box.classList.toggle('open');
}

const partnerSelect = document.getElementById('partner-select');
const chatMessages = document.getElementById('chat-messages');
const chatForm = document.getElementById('chat-form');
const chatInput = document.getElementById('chat-input');

async function loadPartners() {
    try {
        const res = await fetch('/api/messages/partners');
        const data = await res.json();
        partnerSelect.innerHTML = '<option value="">-- Válassz orvost --</option>';
        data.forEach(user => {
            const opt = document.createElement('option');
            opt.value = user.id;
            opt.textContent = user.name;
            partnerSelect.appendChild(opt);
        });
    } catch (err) {
        console.error("Hiba a orvosok betöltésénél:", err);
    }
}

async function loadMessages() {
    const userId = partnerSelect.value;
    if (!userId) return;
    const res = await fetch(`/api/messages?userId=${userId}`);
    const messages = await res.json();
    chatMessages.innerHTML = '';
    messages.forEach(m => {
        const div = document.createElement('div');
        div.classList.add('chat-message');
        div.classList.add(m.isMine ? 'mine' : 'theirs');
        div.textContent = `[${new Date(m.sentAt).toLocaleTimeString()}] ${m.senderName}: ${m.content}`;
        chatMessages.appendChild(div);
    });
    chatMessages.scrollTop = chatMessages.scrollHeight;
}

chatForm?.addEventListener('submit', async (e) => {
    e.preventDefault();
    const userId = partnerSelect.value;
    const content = chatInput.value.trim();
    if (!userId || !content) return;

    const res = await fetch('/api/messages', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ receiverId: userId, content })
    });

    if (res.ok) {
        chatInput.value = '';
        await loadMessages();
    } else {
        alert("Hiba az üzenetküldésnél.");
    }
});

partnerSelect?.addEventListener('change', loadMessages);
setInterval(loadMessages, 5000);
document.addEventListener("DOMContentLoaded", loadPartners);

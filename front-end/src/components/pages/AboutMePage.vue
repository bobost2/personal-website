<template>
  <div class="outer-container">
    <h1>Contacts</h1>
    <p>If you need help with something or just wanna chat, feel free to contact me via the links bellow.
      Note that I won't respond to your request if you only say "Hi", "Hello" or something similar, since I will
      assume that you are either a scammer or a bot. Instead, write what's the reason you are contacting me like:
      "Hello, I have a problem with X, I tried smth, smth and smth, but it didn't worked" or "Hello, I'd like to
      ask you about X, since I'm not sure how to do it". For informal requests like wanting to chat with me, please
      try to tell me "Hello", without sounding like a bot.</p>
    <div class="disclaimer">
      <img src="/icons/mystery.svg" alt="Mystery icon" style="width: 50px"/>
      <div>
        <b>NOTE:</b> Always triple check the source of the messages, since
        I've had a few cases where people have tried to impersonate me!
        Keep in mind that any other social account, which is not listed here,
        is either a parody or an imposter account <i>(not ran by me)</i>.
      </div>
    </div>

    <div class="social-group">
      <div class="social-container">
        <img src="/icons/discord.svg" alt="Discord icon"/>
        <div>Discord:</div>
        <a href="https://discordapp.com/users/258665987212050433">bobost</a>
      </div>

      <div class="social-container">
        <img src="/icons/github.svg" alt="Github icon"/>
        <div>GitHub:</div>
        <a href="https://github.com/bobost2">bobost2</a>
      </div>

      <div class="social-container">
        <img src="/icons/mail.svg" alt="Mail icon"/>
        <div>E-Mail:</div>
        <a href="mailto:admin@bobost.net">admin@bobost.net</a>
      </div>
    </div>
  </div>
</template>

<script setup>

</script>

<style scoped>
  .outer-container {
    padding: 20px 30px;
  }

  h1 {
    font-size: 40px;
    margin: 10px 0;
  }

  p {
    font-size: 20px;
  }

  a {
    color: var(--font-color);
  }

  a:hover {
    color: var(--font-color-2);
  }

  a:active {
    color: var(--font-color-3);
  }

  .disclaimer {
    border: 2px solid var(--font-color);
    margin: 20px 0;
    padding: 15px;
    font-size: 20px;
    display: flex;
    gap: 20px;
  }

  .social-group {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    justify-content: space-between;
  }

  .social-container {
    display: flex;
    border: 2px solid var(--font-color);
    width: fit-content;
    align-items: center;
    flex-wrap: wrap;
    padding: 10px;
    font-size: 20px;
    gap: 10px;
  }
</style>
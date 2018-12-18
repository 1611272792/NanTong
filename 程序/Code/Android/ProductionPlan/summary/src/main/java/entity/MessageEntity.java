package entity;

/**
 * Created by guhh on 2018/3/21.
 */

public class MessageEntity {

    /**
     * StateCode : 100
     * Reason :
     * Message : {"Content":"通知的内容","Color":"#FFFFFF","Size":"18"}
     */

    private int StateCode;
    private String Reason;
    private MessageBean Message;

    public int getStateCode() {
        return StateCode;
    }

    public void setStateCode(int StateCode) {
        this.StateCode = StateCode;
    }

    public String getReason() {
        return Reason;
    }

    public void setReason(String Reason) {
        this.Reason = Reason;
    }

    public MessageBean getMessage() {
        return Message;
    }

    public void setMessage(MessageBean Message) {
        this.Message = Message;
    }

    public static class MessageBean {
        /**
         * Content : 通知的内容
         * Color : #FFFFFF
         * Size : 18
         */

        private String Content;
        private String Color;
        private String Size;

        public String getContent() {
            return Content;
        }

        public void setContent(String Content) {
            this.Content = Content;
        }

        public String getColor() {
            return Color;
        }

        public void setColor(String Color) {
            this.Color = Color;
        }

        public String getSize() {
            return Size;
        }

        public void setSize(String Size) {
            this.Size = Size;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            MessageBean that = (MessageBean) o;

            if (Content != null ? !Content.equals(that.Content) : that.Content != null)
                return false;
            if (Color != null ? !Color.equals(that.Color) : that.Color != null) return false;
            return Size != null ? Size.equals(that.Size) : that.Size == null;
        }

        @Override
        public int hashCode() {
            int result = Content != null ? Content.hashCode() : 0;
            result = 31 * result + (Color != null ? Color.hashCode() : 0);
            result = 31 * result + (Size != null ? Size.hashCode() : 0);
            return result;
        }
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        MessageEntity that = (MessageEntity) o;

        if (StateCode != that.StateCode) return false;
        if (Reason != null ? !Reason.equals(that.Reason) : that.Reason != null) return false;
        return Message != null ? Message.equals(that.Message) : that.Message == null;
    }

    @Override
    public int hashCode() {
        int result = StateCode;
        result = 31 * result + (Reason != null ? Reason.hashCode() : 0);
        result = 31 * result + (Message != null ? Message.hashCode() : 0);
        return result;
    }
}

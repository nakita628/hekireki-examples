class Chat < ApplicationRecord
  self.table_name = "Chat"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :compactionCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :chats
  has_many :messages, class_name: "ChatMessage", foreign_key: "chatId", inverse_of: :chat, dependent: :destroy
  has_many :compactions, class_name: "ChatCompaction", foreign_key: "chatId", inverse_of: :chat, dependent: :destroy
  has_many :memories, class_name: "ChatMemory", foreign_key: "chatId", inverse_of: :chat, dependent: :nullify
  has_many :action_totals, class_name: "ChatActionTotal", foreign_key: "chatId", inverse_of: :chat, dependent: :destroy
  has_many :bulk_inbox_action_jobs, foreign_key: "chatId", inverse_of: :chat, dependent: :nullify
end

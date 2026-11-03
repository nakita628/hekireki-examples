class Chat < ApplicationRecord
  self.table_name = "Chat"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :team, foreign_key: "teamId", inverse_of: :chats
  belongs_to :document, foreign_key: "documentId", optional: true, inverse_of: :chats
  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :chats
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :chats
  belongs_to :view, foreign_key: "viewId", optional: true, inverse_of: :chats
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :chats
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :chats
  has_many :messages, class_name: "ChatMessage", foreign_key: "chatId", inverse_of: :chat, dependent: :destroy
end

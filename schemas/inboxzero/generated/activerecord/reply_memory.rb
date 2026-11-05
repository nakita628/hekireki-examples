class ReplyMemory < ApplicationRecord
  self.table_name = "ReplyMemory"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isLearnedStyleEvidence, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :kind, { fact: "FACT", preference: "PREFERENCE", procedure: "PROCEDURE" }, validate: true
  enum :scopeType, { global: "GLOBAL", sender: "SENDER", domain: "DOMAIN", topic: "TOPIC" }, validate: true

  validates :content, presence: true
  validates :scopeValue, presence: true, uniqueness: { scope: [ :emailAccountId, :kind, :scopeType, :content ] }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :reply_memories
  has_many :sources, class_name: "ReplyMemorySource", foreign_key: "replyMemoryId", inverse_of: :reply_memory,
                     dependent: :destroy
end

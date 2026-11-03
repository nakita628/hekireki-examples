class Messages < ApplicationRecord
  self.table_name = "Messages"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :from, { buyer: "BUYER", seller: "SELLER" }, validate: true

  belongs_to :group, class_name: "MessagesGroup", foreign_key: "groupId", inverse_of: :messages
  has_many :posts, foreign_key: "lastMessageId", inverse_of: :last_message, dependent: :nullify
end

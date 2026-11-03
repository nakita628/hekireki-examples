class ConversationView < ApplicationRecord
  self.table_name = "ConversationView"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :conversationId, uniqueness: { scope: :viewId }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :views
  belongs_to :view, foreign_key: "viewId", inverse_of: :conversation_views
end

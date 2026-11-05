class DigestItem < ApplicationRecord
  self.table_name = "DigestItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :messageId, presence: true
  validates :threadId, presence: true, uniqueness: { scope: [ :digestId, :messageId ] }
  validates :content, presence: true

  belongs_to :digest, class_name: "DigestRecord", foreign_key: "digestId", inverse_of: :items
  belongs_to :action, class_name: "ExecutedAction", foreign_key: "actionId", optional: true, inverse_of: :digest_items
  belongs_to :cold_email, foreign_key: "coldEmailId", optional: true, inverse_of: :digest_items
end

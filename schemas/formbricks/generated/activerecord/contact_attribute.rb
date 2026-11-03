class ContactAttribute < ApplicationRecord
  self.table_name = "ContactAttribute"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :contactId, uniqueness: { scope: :attributeKeyId }
  validates :value, presence: true

  belongs_to :attribute_key, class_name: "ContactAttributeKey", foreign_key: "attributeKeyId", inverse_of: :attributes
  belongs_to :contact, foreign_key: "contactId", inverse_of: :attributes
end

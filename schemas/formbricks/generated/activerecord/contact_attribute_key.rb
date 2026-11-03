class ContactAttributeKey < ApplicationRecord
  self.table_name = "ContactAttributeKey"
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :isUnique, default: false

  enum :type, { default: "default", custom: "custom" }, default: :custom, validate: true
  enum :dataType, { string: "string", number: "number", date: "date" }, default: :string, validate: true

  validates :key, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :contact_attribute_keys
  has_many :contact_attributes, foreign_key: "attributeKeyId", inverse_of: :attribute_key, dependent: :destroy
  has_many :attribute_filters, class_name: "SurveyAttributeFilter", foreign_key: "attributeKeyId",
                               inverse_of: :attribute_key, dependent: :destroy
end

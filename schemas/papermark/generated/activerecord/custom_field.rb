class CustomField < ApplicationRecord
  self.table_name = "CustomField"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :required, default: false
  attribute :disabled, default: false
  attribute :orderIndex, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { short_text: "SHORT_TEXT", long_text: "LONG_TEXT", number: "NUMBER", phone_number: "PHONE_NUMBER", url: "URL", checkbox: "CHECKBOX", select: "SELECT", multi_select: "MULTI_SELECT" },
       prefix: true, validate: true

  validates :identifier, presence: true
  validates :label, presence: true

  belongs_to :link, foreign_key: "linkId", inverse_of: :custom_fields
end

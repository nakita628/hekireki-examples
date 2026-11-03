class Field < ApplicationRecord
  self.table_name = "Field"
  self.inheritance_column = nil

  attribute :secondaryId, default: -> { Cuid.generate }
  attribute :positionX, default: BigDecimal("0")
  attribute :positionY, default: BigDecimal("0")
  attribute :width, default: BigDecimal("-1")
  attribute :height, default: BigDecimal("-1")

  enum :type, { signature: "SIGNATURE", free_signature: "FREE_SIGNATURE", initials: "INITIALS", name: "NAME", email: "EMAIL", date: "DATE", text: "TEXT", number: "NUMBER", radio: "RADIO", checkbox: "CHECKBOX", dropdown: "DROPDOWN" },
       prefix: true, validate: true

  validates :secondaryId, uniqueness: true
  validates :page, presence: true
  validates :customText, presence: true
  validates :inserted, inclusion: { in: [ true, false ] }

  belongs_to :envelope_item, foreign_key: "envelopeItemId", inverse_of: :field
  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :fields
  belongs_to :recipient, foreign_key: "recipientId", inverse_of: :fields
  has_one :signature, foreign_key: "fieldId", inverse_of: :field, dependent: :destroy
end

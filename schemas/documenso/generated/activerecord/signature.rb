class Signature < ApplicationRecord
  self.table_name = "Signature"

  validates :fieldId, uniqueness: true

  belongs_to :recipient, foreign_key: "recipientId", inverse_of: :signatures
  belongs_to :field, foreign_key: "fieldId", inverse_of: :signature
end

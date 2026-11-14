class IdentifierReservation < ApplicationRecord
  self.table_name = "IdentifierReservation"
  self.primary_key = "normalizedValue"

  alias_attribute :created_at, :createdAt

  validates :userId, presence: true
  validates :identifierId, presence: true
  validates :commandId, presence: true
end

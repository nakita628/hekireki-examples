class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"

  attribute :failedAttempts, default: 0

  validates :identifier, presence: true, uniqueness: { scope: :token }
  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true
end

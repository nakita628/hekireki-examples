class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"

  attribute :token, default: -> { Cuid.generate }

  validates :deviceIdentifier, presence: true, uniqueness: { scope: :token }
  validates :token, uniqueness: true
  validates :expiresOn, presence: true

  belongs_to :user, foreign_key: "userUid", inverse_of: :verification_tokens
end

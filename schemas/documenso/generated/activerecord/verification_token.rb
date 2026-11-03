class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"

  attribute :secondaryId, default: -> { Cuid.generate }
  attribute :completed, default: false
  alias_attribute :created_at, :createdAt

  validates :secondaryId, uniqueness: true
  validates :identifier, presence: true
  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :verification_tokens
end

class TwoFactor < ApplicationRecord
  self.table_name = "two_factor"

  attribute :verified, default: true
  attribute :failedVerificationCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true
  validates :secret, presence: true
  validates :backupCodes, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :two_factor
end

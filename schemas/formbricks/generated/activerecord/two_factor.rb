class TwoFactor < ApplicationRecord
  self.table_name = "TwoFactor"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: true
  attribute :failedVerificationCount, default: 0

  validates :secret, presence: true
  validates :backupCodes, presence: true
  validates :userId, uniqueness: true
  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :two_factor
end

class TwoFactor < ApplicationRecord
  self.table_name = "TwoFactor"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :verified, default: true
  attribute :failedVerificationCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :secret, presence: true
  validates :backupCodes, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :two_factors
end

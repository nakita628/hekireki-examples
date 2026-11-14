class SignInAttemptLock < ApplicationRecord
  self.table_name = "SignInAttemptLock"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :failedCount, default: 0
  attribute :consecutiveLockouts, default: 0
  attribute :heldForReview, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifierHash, presence: true, uniqueness: true
end

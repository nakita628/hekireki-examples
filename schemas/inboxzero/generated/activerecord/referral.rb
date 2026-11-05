class Referral < ApplicationRecord
  self.table_name = "Referral"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", completed: "COMPLETED" }, default: :pending, validate: true

  validates :referredUserId, uniqueness: true
  validates :referralCodeUsed, presence: true

  belongs_to :referrer_user, class_name: "User", foreign_key: "referrerUserId", inverse_of: :referrals_made
  belongs_to :referred_user, class_name: "User", foreign_key: "referredUserId", inverse_of: :referral_received
end

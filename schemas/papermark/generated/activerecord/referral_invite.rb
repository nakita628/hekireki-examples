class ReferralInvite < ApplicationRecord
  self.table_name = "ReferralInvite"

  attribute :id, default: -> { Cuid.generate }

  validates :scopeId, presence: true, uniqueness: { scope: :email }
  validates :email, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :referral_invites
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :referral_invites
end

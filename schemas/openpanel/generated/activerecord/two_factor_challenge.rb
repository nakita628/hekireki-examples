class TwoFactorChallenge < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :expiresAt, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :two_factor_challenges
end

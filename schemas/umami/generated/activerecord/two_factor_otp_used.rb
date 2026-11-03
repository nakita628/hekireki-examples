class TwoFactorOtpUsed < ApplicationRecord
  self.table_name = "two_factor_otp_used"

  attribute :id, default: -> { Cuid.generate }

  validates :otp, presence: true, uniqueness: { scope: :user_id }
  validates :expires_at, presence: true

  belongs_to :user, inverse_of: :two_factor_otp_useds
end

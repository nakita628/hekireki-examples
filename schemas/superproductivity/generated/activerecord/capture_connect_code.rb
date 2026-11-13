class CaptureConnectCode < ApplicationRecord
  self.primary_key = "user_id"

  validates :code_hash, presence: true, uniqueness: true
  validates :token_version, presence: true
  validates :expires_at, presence: true

  belongs_to :user
end

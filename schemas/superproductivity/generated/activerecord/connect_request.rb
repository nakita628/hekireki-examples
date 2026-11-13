class ConnectRequest < ApplicationRecord
  attribute :status, default: "pending"

  validates :poll_secret_hash, presence: true
  validates :match_code, presence: true
  validates :device_name, presence: true
  validates :platform, presence: true
  validates :expires_at, presence: true

  belongs_to :user, optional: true, inverse_of: :connect_requests
end

class CaptureGrant < ApplicationRecord
  validates :kind, presence: true
  validates :client_name, presence: true
  validates :secret_hash, presence: true
  validates :created_at, presence: true
  validates :expires_at, presence: true

  belongs_to :user, inverse_of: :capture_grants
end

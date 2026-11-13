class CaptureKey < ApplicationRecord
  self.primary_key = "user_id"

  validates :public_key, presence: true
  validates :fingerprint, presence: true
  validates :created_at, presence: true

  belongs_to :user
end

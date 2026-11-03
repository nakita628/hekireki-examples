class Verification < ApplicationRecord
  validates :identifier, presence: true, uniqueness: true
  validates :value, presence: true
  validates :expires_at, presence: true
end

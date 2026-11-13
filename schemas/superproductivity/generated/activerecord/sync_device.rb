class SyncDevice < ApplicationRecord
  self.primary_key = %w[user_id client_id]

  attribute :last_acked_seq, default: 0

  validates :last_seen_at, presence: true
  validates :created_at, presence: true

  belongs_to :user, inverse_of: :devices
end

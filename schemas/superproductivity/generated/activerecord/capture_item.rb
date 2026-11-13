class CaptureItem < ApplicationRecord
  validates :grant_id, presence: true
  validates :ciphertext, presence: true
  validates :task_id, presence: true
  validates :created_at, presence: true

  belongs_to :user, inverse_of: :capture_items
end

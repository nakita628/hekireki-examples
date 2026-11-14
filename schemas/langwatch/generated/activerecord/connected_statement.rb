class ConnectedStatement < ApplicationRecord
  self.table_name = "ConnectedStatement"

  attribute :id, default: -> { Nanoid.generate }

  validates :accountId, presence: true, uniqueness: { scope: :month }
  validates :month, presence: true
  validates :sentAt, presence: true
end

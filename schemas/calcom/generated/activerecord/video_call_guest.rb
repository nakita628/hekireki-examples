class VideoCallGuest < ApplicationRecord
  self.table_name = "VideoCallGuest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :bookingUid, presence: true, uniqueness: { scope: :email }
  validates :email, presence: true
  validates :name, presence: true
end

class ClippingClip < ApplicationRecord
  self.table_name = "ClippingClip"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :status, default: "pending"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true
  validates :content, presence: true
  validates :start, presence: true
  validates :end, presence: true

  belongs_to :clipping, foreign_key: "clippingId", inverse_of: :clips
end

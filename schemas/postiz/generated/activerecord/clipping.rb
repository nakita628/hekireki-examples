class Clipping < ApplicationRecord
  self.table_name = "Clipping"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :status, default: "analysing"
  attribute :maxClips, default: 5
  attribute :fit, default: "blur"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :url, presence: true
  validates :integrations, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :clippings
  has_many :clips, class_name: "ClippingClip", foreign_key: "clippingId", inverse_of: :clipping,
                   dependent: :restrict_with_error
end

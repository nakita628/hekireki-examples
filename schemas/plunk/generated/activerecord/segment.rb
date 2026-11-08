class Segment < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :trackMembership, default: false
  attribute :memberCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { dynamic: "DYNAMIC", static: "STATIC" }, default: :dynamic, validate: true

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :segments
  has_many :memberships, class_name: "SegmentMembership", foreign_key: "segmentId", inverse_of: :segment,
                         dependent: :destroy
  has_many :campaigns, foreign_key: "segmentId", inverse_of: :segment, dependent: :nullify
end

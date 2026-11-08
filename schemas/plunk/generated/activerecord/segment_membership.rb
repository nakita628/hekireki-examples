class SegmentMembership < ApplicationRecord
  self.primary_key = %w[contactId segmentId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :contact, foreign_key: "contactId", inverse_of: :segment_memberships
  belongs_to :segment, foreign_key: "segmentId", inverse_of: :memberships
end

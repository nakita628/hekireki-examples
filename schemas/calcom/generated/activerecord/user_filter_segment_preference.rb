class UserFilterSegmentPreference < ApplicationRecord
  self.table_name = "UserFilterSegmentPreference"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :tableIdentifier, presence: true, uniqueness: { scope: :userId }

  belongs_to :user, foreign_key: "userId", inverse_of: :filter_segment_preferences
  belongs_to :segment, class_name: "FilterSegment", foreign_key: "segmentId", optional: true,
                       inverse_of: :user_preferences
end

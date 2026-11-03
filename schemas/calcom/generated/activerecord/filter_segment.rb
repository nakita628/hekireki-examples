class FilterSegment < ApplicationRecord
  self.table_name = "FilterSegment"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scope, { user: "USER", team: "TEAM" }, validate: true

  validates :name, presence: true
  validates :tableIdentifier, presence: true
  validates :perPage, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :filter_segments
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :filter_segments
  has_many :user_preferences, class_name: "UserFilterSegmentPreference", foreign_key: "segmentId", inverse_of: :segment,
                              dependent: :destroy
end

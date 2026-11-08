class Track < ApplicationRecord
  self.table_name = "Track"

  attribute :visible, default: true
  attribute :isComingSoon, default: false

  validates :name, presence: true, uniqueness: true
  validates :slug, presence: true, uniqueness: true
  validates :description, presence: true

  has_many :track_challenges, foreign_key: "trackId", inverse_of: :track, dependent: :nullify
  has_and_belongs_to_many :enrolled_users, class_name: "User", join_table: "_TrackToUser", foreign_key: "A",
                                           association_foreign_key: "B"
end

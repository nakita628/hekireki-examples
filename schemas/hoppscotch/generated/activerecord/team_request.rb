class TeamRequest < ApplicationRecord
  self.table_name = "TeamRequest"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  validates :title, presence: true
  validates :request, exclusion: { in: [ nil ] }
  validates :orderIndex, presence: true, uniqueness: { scope: [ :teamID, :collectionID ] }

  belongs_to :collection, class_name: "TeamCollection", foreign_key: "collectionID", inverse_of: :requests
  belongs_to :team, foreign_key: "teamID", inverse_of: :requests
end

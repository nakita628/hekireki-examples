class TeamCollection < ApplicationRecord
  self.table_name = "TeamCollection"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  validates :title, presence: true
  validates :orderIndex, presence: true,
                         uniqueness: { scope: [ :teamID, :parentID ], conditions: -> { where.not(parentID: nil) } }

  belongs_to :parent, class_name: "TeamCollection", foreign_key: "parentID", optional: true, inverse_of: :children
  belongs_to :team, foreign_key: "teamID", inverse_of: :collections
  has_many :children, class_name: "TeamCollection", foreign_key: "parentID", inverse_of: :parent, dependent: :destroy
  has_many :requests, class_name: "TeamRequest", foreign_key: "collectionID", inverse_of: :collection,
                      dependent: :destroy
end

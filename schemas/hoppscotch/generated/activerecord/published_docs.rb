class PublishedDocs < ApplicationRecord
  self.table_name = "PublishedDocs"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  enum :workspaceType, { user: "USER", team: "TEAM" }, validate: true

  validates :slug, presence: true, uniqueness: { scope: :version }
  validates :title, presence: true
  validates :collectionID, presence: true
  validates :creatorUid, presence: true
  validates :version, presence: true
  validates :autoSync, inclusion: { in: [ true, false ] }
  validates :workspaceID, presence: true
end

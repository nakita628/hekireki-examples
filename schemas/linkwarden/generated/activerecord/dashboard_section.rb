class DashboardSection < ApplicationRecord
  self.table_name = "DashboardSection"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { stats: "STATS", recent_links: "RECENT_LINKS", pinned_links: "PINNED_LINKS", collection: "COLLECTION" },
       validate: true

  validates :userId, uniqueness: { scope: :collectionId, conditions: -> { where.not(collectionId: nil) } }
  validates :order, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :dashboard_sections
  belongs_to :collection, foreign_key: "collectionId", optional: true, inverse_of: :dashboard_section
end

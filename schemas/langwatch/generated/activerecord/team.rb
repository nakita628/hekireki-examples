class Team < ApplicationRecord
  self.table_name = "Team"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :isPersonal, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true
  validates :ownerUserId, length: { maximum: 255 }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :teams
  belongs_to :owner_user, class_name: "User", foreign_key: "ownerUserId", optional: true, inverse_of: :personal_teams
  has_many :members, class_name: "TeamUser", foreign_key: "teamId", inverse_of: :team, dependent: :restrict_with_error
  has_many :projects, foreign_key: "teamId", inverse_of: :team, dependent: :restrict_with_error
  has_many :ingestion_sources, foreign_key: "teamId", inverse_of: :team, dependent: :nullify
  has_many :ai_tool_entry_teams, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
end

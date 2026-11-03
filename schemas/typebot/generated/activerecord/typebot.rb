class Typebot < ApplicationRecord
  self.table_name = "Typebot"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isArchived, default: false
  attribute :isClosed, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :groups, exclusion: { in: [ nil ] }
  validates :variables, exclusion: { in: [ nil ] }
  validates :edges, exclusion: { in: [ nil ] }
  validates :theme, exclusion: { in: [ nil ] }
  validates :settings, exclusion: { in: [ nil ] }
  validates :publicId, uniqueness: { allow_nil: true }
  validates :customDomain, uniqueness: { allow_nil: true }

  belongs_to :folder, class_name: "DashboardFolder", foreign_key: "folderId", optional: true, inverse_of: :typebots
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :typebots
  belongs_to :space, foreign_key: "spaceId", optional: true, inverse_of: :typebots
  has_one :published_typebot, class_name: "PublicTypebot", foreign_key: "typebotId", inverse_of: :typebot,
                              dependent: :destroy
  has_many :collaborators, class_name: "CollaboratorsOnTypebots", foreign_key: "typebotId", inverse_of: :typebot,
                           dependent: :destroy
  has_many :invitations, foreign_key: "typebotId", inverse_of: :typebot, dependent: :destroy
  has_many :results, foreign_key: "typebotId", inverse_of: :typebot, dependent: :destroy
  has_many :webhooks, foreign_key: "typebotId", inverse_of: :typebot, dependent: :destroy
  has_many :banned_ips, foreign_key: "responsibleTypebotId", inverse_of: :responsible_typebot,
                        dependent: :restrict_with_error
end

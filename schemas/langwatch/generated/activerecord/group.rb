class Group < ApplicationRecord
  self.table_name = "Group"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :organizationId }
  validates :scimConnectionId,
            uniqueness: { scope: :externalId, conditions: -> { where.not(externalId: nil) }, allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :groups
  has_many :members, class_name: "GroupMembership", foreign_key: "groupId", inverse_of: :group, dependent: :destroy
  has_many :role_bindings, foreign_key: "groupId", inverse_of: :group, dependent: :destroy
end

class Member < ApplicationRecord
  self.table_name = "Member"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :role, default: "member"
  attribute :allowOrgAdminAnalytics, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, uniqueness: { scope: :emailAccountId }
  validates :emailAccountId, uniqueness: true
  validates :emailAccountId, uniqueness: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :members
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :members
  has_many :conversations, foreign_key: "publisherId", inverse_of: :publisher, dependent: :destroy
  has_many :conversation_participants, foreign_key: "memberId", inverse_of: :member, dependent: :destroy
  has_many :conversation_comments, foreign_key: "authorMemberId", inverse_of: :author, dependent: :nullify
end

class OrganisationMemberInvite < ApplicationRecord
  self.table_name = "OrganisationMemberInvite"

  alias_attribute :created_at, :createdAt

  enum :status, { accepted: "ACCEPTED", pending: "PENDING", declined: "DECLINED" }, default: :pending, validate: true
  enum :organisationRole, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER" }, validate: true

  validates :email, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :invites
end

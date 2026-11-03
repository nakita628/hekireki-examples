class OrganisationMember < ApplicationRecord
  self.table_name = "OrganisationMember"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: { scope: :organisationId }

  belongs_to :user, foreign_key: "userId", inverse_of: :organisation_member
  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :members
  has_many :organisation_group_members, foreign_key: "organisationMemberId", inverse_of: :organisation_member,
                                        dependent: :destroy
end

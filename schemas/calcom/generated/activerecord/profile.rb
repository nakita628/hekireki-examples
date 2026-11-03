class Profile < ApplicationRecord
  self.table_name = "Profile"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :uid, presence: true
  validates :userId, uniqueness: { scope: :organizationId }
  validates :username, presence: true, uniqueness: { scope: :organizationId }

  belongs_to :user, foreign_key: "userId", inverse_of: :profiles
  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", inverse_of: :org_profiles
  has_one :moved_from_user, class_name: "User", foreign_key: "movedToProfileId", inverse_of: :moved_to_profile,
                            dependent: :nullify
  has_many :event_types, foreign_key: "profileId", inverse_of: :profile, dependent: :nullify
end

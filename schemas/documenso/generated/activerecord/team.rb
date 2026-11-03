class Team < ApplicationRecord
  self.table_name = "Team"

  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :url, presence: true, uniqueness: true
  validates :teamGlobalSettingsId, uniqueness: true

  belongs_to :avatar_image, foreign_key: "avatarImageId", optional: true, inverse_of: :team
  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :teams
  belongs_to :team_global_settings, foreign_key: "teamGlobalSettingsId", inverse_of: :team
  has_one :team_email, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_one :email_verification, class_name: "TeamEmailVerification", foreign_key: "teamId", inverse_of: :team,
                               dependent: :destroy
  has_one :profile, class_name: "TeamProfile", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :envelopes, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :folders, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :api_tokens, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :webhooks, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :team_groups, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
end

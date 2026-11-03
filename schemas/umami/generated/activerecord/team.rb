class Team < ApplicationRecord
  self.table_name = "team"
  self.primary_key = "team_id"

  attribute :two_factor_required, default: false

  validates :name, presence: true, length: { maximum: 50 }
  validates :access_code, length: { maximum: 50 }, uniqueness: { allow_nil: true }
  validates :logo_url, length: { maximum: 2183 }

  has_many :websites, dependent: :nullify
  has_many :members, class_name: "TeamUser", inverse_of: :team, dependent: :restrict_with_error
  has_many :links, dependent: :nullify
  has_many :pixels, dependent: :nullify
  has_many :boards, dependent: :nullify
end

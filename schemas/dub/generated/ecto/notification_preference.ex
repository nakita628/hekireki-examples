defmodule Dub.NotificationPreference do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          link_usage_summary: boolean(),
          domain_configuration_updates: boolean(),
          project_user: Dub.ProjectUsers.t() | nil
        }

  schema "NotificationPreference" do
    field(:id, :string, primary_key: true)
    field(:link_usage_summary, :boolean, default: true, source: :linkUsageSummary)
    field(:domain_configuration_updates, :boolean, default: true, source: :domainConfigurationUpdates)
    field(:project_user_id, :string, source: :projectUserId)
    belongs_to(:project_user, Dub.ProjectUsers, foreign_key: :project_user_id, define_field: false, type: :string)
  end
end
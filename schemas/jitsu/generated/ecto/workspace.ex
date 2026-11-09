defmodule Jitsu.Workspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          deleted: boolean(),
          slug: String.t() | nil,
          features_enabled: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace_access: [Jitsu.WorkspaceAccess.t()],
          workspace_options: [Jitsu.WorkspaceOptions.t()],
          configuration_object: [Jitsu.ConfigurationObject.t()],
          configuration_object_link: [Jitsu.ConfigurationObjectLink.t()],
          preferences: [Jitsu.UserPreferences.t()],
          workspace_user_properties: [Jitsu.WorkspaceUserProperties.t()],
          profile_builders: [Jitsu.ProfileBuilder.t()],
          oidc_login_groups: [Jitsu.OidcLoginGroup.t()]
        }

  schema "Workspace" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:deleted, :boolean, default: false)
    field(:slug, :string)
    field(:features_enabled, {:array, :string}, source: :featuresEnabled)
    has_many(:workspace_access, Jitsu.WorkspaceAccess, foreign_key: :workspace_id, references: :id)
    has_many(:workspace_options, Jitsu.WorkspaceOptions, foreign_key: :workspace_id, references: :id)
    has_many(:configuration_object, Jitsu.ConfigurationObject, foreign_key: :workspace_id, references: :id)
    has_many(:configuration_object_link, Jitsu.ConfigurationObjectLink, foreign_key: :workspace_id, references: :id)
    has_many(:preferences, Jitsu.UserPreferences, foreign_key: :workspace_id, references: :id)
    has_many(:workspace_user_properties, Jitsu.WorkspaceUserProperties, foreign_key: :workspace_id, references: :id)
    has_many(:profile_builders, Jitsu.ProfileBuilder, foreign_key: :workspace_id, references: :id)
    has_many(:oidc_login_groups, Jitsu.OidcLoginGroup, foreign_key: :workspace_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
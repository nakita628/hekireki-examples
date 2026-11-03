defmodule Langfuse.GatewayConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          ingestion_mode: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Langfuse.Organization.t() | nil,
          default_ingestion_project: Langfuse.Project.t() | nil
        }

  schema "gateway_configs" do
    field(:ingestion_mode, Ecto.Enum, values: [NONE: "none", USAGE: "usage", FULL: "full"], default: :USAGE)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :organization_id, type: :string)
    belongs_to(:default_ingestion_project, Langfuse.Project, foreign_key: :default_ingestion_project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end
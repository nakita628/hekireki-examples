defmodule Trigger.WorkerDeploymentPromotion do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          label: String.t(),
          deployment: Trigger.WorkerDeployment.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "WorkerDeploymentPromotion" do
    field(:id, :string, primary_key: true)
    field(:label, :string)
    field(:deployment_id, :string, source: :deploymentId)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:deployment, Trigger.WorkerDeployment, foreign_key: :deployment_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
  end
end